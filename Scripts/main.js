(() => {
    window.onload = () => {
        bootstrap();
    };
})();

var NUMBER_OF_ROWS = 0;
var DEPENDENCIES = [];

function bootstrap() {
    addRow();
}

function addRow () {
    $('#rules').append(createRow(NUMBER_OF_ROWS));
    NUMBER_OF_ROWS++;
}

function addDependency (ev) {
    var ruleId = ev.target.id.split('_')[1];
    var dependencyId = DEPENDENCIES[ruleId];

    $('<input type="text" />')
        .attr('id', "rule_" + ruleId + "_dependency_" + dependencyId)
        .addClass('dependency')
        .insertBefore(ev.target);

    DEPENDENCIES[ruleId]++;
}

function createRow (id) {
    var dependencyId = 0;

    if (DEPENDENCIES[id]) {
        dependencyId = DEPENDENCIES[id];
    } else {
        DEPENDENCIES[id] = 1;
    }

    var div = $('<div></div>')
                .attr('id', "rule_" + id)
                .addClass('rule');

    var fact = $('<input type="text" />')
                .attr('id', "rule_" + id + "_fact")
                .addClass('fact');
                
    var dependency = $('<input type="text" />')
                        .attr('id', "rule_" + id + "_dependency_" + dependencyId)
                        .addClass('dependency');
    
    var dependencyButton = $('<button>Add dependency</button>')
                            .attr('id', 'rule_' + id + '_button')
                            .addClass('dependencyButton')
                            .on('click', (ev) => addDependency(ev));

    var separator = $('<span>:</span>');

    div.append(fact, separator, dependency, dependencyButton);

    return div;
} 

function submit () {
    var data = [];

    for (var i = 0; i < NUMBER_OF_ROWS; i++) {
        var dependencies = [];
        var item = $('#rule_' + i + '_fact').val().replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, '-');

        if (item.length > 0) {

            for (var j = 0; j < DEPENDENCIES[i]; j++) {
                var value = $('#rule_' + i + '_dependency_' + j).val().replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, '-');
                
                if (value.length > 0) {
                    dependencies.push(value);
                }
            }

            if (dependencies.length > 0) {
                data.push({item: item, dependencies: dependencies});
            }
        }
    }

    // in case current url is not / but something different like /index.html
    var endpoint = "http://" + document.location.href.split('/')[2] + "/";

    $.ajax({
        contentType: 'application/json',
        url: endpoint + 'api/solve',
        data: JSON.stringify(data),
        method: 'POST'
    }).done((result) => {
        var div = $('.result')
                    .empty()
                    .css('color', '#000');

        for (var i = 0; i < result.result.length; i++) {
            div.append("<div>" + result.result[i].item + ' -> ' + result.result[i].dependencies + "</div>");
        }
    }).fail((error) => {
        var div = $('.result')
                    .empty()
                    .css('color', 'red');

        for (var i = 0; i < error.responseJSON.errors.length; i++) {
            if (error.responseJSON.errors[i].item.length > 0) {
                div.append('<div>' + error.responseJSON.errors[i].item + ' -> ' + error.responseJSON.errors[i].errors + '</div>');
            } else {
                div.append('<div>' + error.responseJSON.errors[i].errors + '</div>');
            }
        }
    });
}