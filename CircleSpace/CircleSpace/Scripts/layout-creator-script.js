var layoutTypeSelected;

//Add cloning feature to the drag and drop script so the creation area isn't depleted as things are dragged.

function EditTextOfElement(element) {
    var textArea = document.createElement('input');
    var textAreaJQ = $(textArea);
    textArea.setAttribute('type', 'text');
    textArea.value = element.textContent;

    $(element).replaceWith(
        textArea
    );

    textAreaJQ.blur(function () {
        element.textContent = textArea.value;
        textAreaJQ.replaceWith(element);
    });

    textArea.focus();
}

function RemoveElement(element) {
    $(element).remove();
}

window.addEventListener('load', function (event) {

    $('#draggableItems').each(function (index, element) {
        MakeElementsDraggable(element);
        $(element).children().mouseup(function (event) {
            switch (event.which) {
                case 1:
                    EditTextOfElement(event.target);
                    //'Left Mouse button pressed.
                    break;
                case 3:
                    RemoveElement(event.target);
                    //'Right Mouse button pressed.'
                    break;
            }
        });
    });

    MakeElementDropTarget($('#live-preview-area')[0]);

    var typeSelector = $('#type-selector')[0];
    $(typeSelector).click(
        function (event) {
            layoutTypeSelected = typeSelector.options[typeSelector.selectedIndex].value;
        }
    );

    $(typeSelector).trigger('click');

    console.log(layoutTypeSelected);
});

function SaveLayout() {

}

