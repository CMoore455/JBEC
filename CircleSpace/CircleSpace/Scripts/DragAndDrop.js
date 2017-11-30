var dragTarget;

function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    dragTarget = ev.target;
}

function drop(ev) {
    ev.preventDefault();
    ev.target.appendChild(dragTarget);
}

function MakeElementsDraggable(context) {
    var draggableElements = $("h1, h2, h3, h4, h5, h6, p, ul, ol, li, img", context);
    draggableElements.each(function (index, elem) {

        var htmlElement = elem;
        htmlElement.setAttribute('draggable','true');
        htmlElement.setAttribute('ondragstart', 'drag(event)');
    });    
};

function MakeElementDropTarget(element) {
    $(element).addClass("dropTarget");
    element.setAttribute('ondrop', 'drop(event)');
    element.setAttribute('ondragover', 'allowDrop(event)');

}