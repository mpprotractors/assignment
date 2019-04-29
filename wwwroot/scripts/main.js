(() => {
    window.onload = () => {
        bootstrap();
    };
})();

function bootstrap() {
    let descriptorInput = document.getElementById('descriptor-input');
    let submitButton = document.getElementById('submit-btn');
    let dependenciesOutput = document.getElementById('dependencies-output');
    let errorOutput = document.getElementById('error-output');

    submitButton.onclick = () => {
        if (descriptorInput.value) {
            errorOutput.innerText = '';

            submitDescriptor(descriptorInput.value, res => {
                let formattedDeps = formatResponse(res);
                dependenciesOutput.value = formattedDeps;
                descriptorInput.value = '';
            }, err => {
                errorOutput.innerText = err;
            });
        }
    };
}

function submitDescriptor(dependencyDescriptor, successCallback, errorCallback) {
    let http = new XMLHttpRequest();
    let url = '/api/dependency-resolution/descriptors';
    let params = 'B -> CE';
    http.open('POST', url, true);

    //Send the proper header information along with the request
    http.setRequestHeader('Content-type', 'application/json');

    http.onreadystatechange = () => {
        if (http.readyState === 4) {
            if (http.status === 200) {
                successCallback(http.responseText);
            } else {
                errorCallback(http.responseText);
            }
        }
    }

    http.send(JSON.stringify(dependencyDescriptor));
}

function formatResponse(response) {
    let dependencies = JSON.parse(response);

    let formattedDeps = dependencies.map(d => {
        return d.item + ' -> ' + d.dependencies.join('');
    });

    return formattedDeps.join('\n');
}