(() => {
    window.onload = () => {
        bootstrap();
    };
})();

function bootstrap() {
    class Node {
        constructor(name) {
            this.name = name;
            this.edges = [];
            this.addEdge = node => this.edges.push(node);
        }
    }

    let a = new Node('A');
    let b = new Node('B');
    let c = new Node('C');
    let d = new Node('D');
    let e = new Node('E');
    let f = new Node('F');
    let g = new Node('G');
    let h = new Node('H');

    a.addEdge(b);
    a.addEdge(c);
    b.addEdge(c);
    b.addEdge(e);
    c.addEdge(g);
    d.addEdge(a);
    d.addEdge(f);
    e.addEdge(f);
    f.addEdge(h);

    let resolveNodes = (node, resolved, unresolved) => {
        unresolved.push(node);
        node.edges.forEach(edge => {
            if (!resolved.some(item => item === edge)) {
                if (unresolved.some(item => item === edge)) {
                    return console.log('Exeption error', node.name, edge.name);
                }
                return resolveNodes(edge, resolved, unresolved);
            }
        });
        resolved.push(node);
        unresolved.pop(node);
    }

    let requiredNodes = [a, b, c, d, e, f];

    requiredNodes.forEach(requiredNode => {
        let resolvedNodes = [];
        let br = document.createElement("br");
        let sp = document.createElement("span");
        sp.innerHTML = "Dependency " + requiredNode.name + " resolve order: ";
        document.body.appendChild(sp);
        resolveNodes(requiredNode, resolvedNodes, []);
        resolvedNodes.forEach(resolvedNode => {
            let span = document.createElement("span");
            span.innerHTML = resolvedNode.name;
            document.body.appendChild(span);
        });
        document.body.appendChild(br);
    });
}
