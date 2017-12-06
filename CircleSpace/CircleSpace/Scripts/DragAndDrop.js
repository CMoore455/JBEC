var dragTarget;
var ShouldCloneMap = new Map();
var Callbacks = [];


function onClonedElement(func) {

    Callbacks.push(func);
}

function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    dragTarget = ev.target;
}

function drop(ev) {
    ev.preventDefault();
    if (ShouldCloneMap.get(dragTarget)) {
        var clonedTarget = dragTarget.cloneNode(true);
        ev.target.appendChild(clonedTarget);
        Callbacks.forEach(function (callback) {
            callback(clonedTarget);
        });
    }
    else {
        ev.target.appendChild(dragTarget);
    }

}

function MakeElementsDraggable(context, shouldClone) {
    var draggableElements = $("h1, h2, h3, h4, h5, h6, p, ul, ol, li, img, a", context);
    draggableElements.each(function (index, elem) {

        var htmlElement = elem;
        htmlElement.setAttribute('draggable', 'true');
        htmlElement.setAttribute('ondragstart', 'drag(event)');
        ShouldCloneMap.set(htmlElement, shouldClone);
    });
};


function MakeElementDropTarget(element) {
    $(element).addClass("dropTarget");
    element.setAttribute('ondrop', 'drop(event)');
    element.setAttribute('ondragover', 'allowDrop(event)');

}