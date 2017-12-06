var layoutTypeSelected, layoutId;

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
        $(element).mouseup(EditOrRemoveTextOnClick);
        textAreaJQ.replaceWith(element);
    });

    textArea.focus();
}

function RemoveElement(element) {
    $(element).remove();
}

function EditOrRemoveTextOnClick(event) {
    switch (event.which) {
        case 1:
            if (event.shiftKey) {
                RemoveElement(event.target);
            } else {
                EditTextOfElement(event.target);
            }
            break;
    }
}

window.addEventListener('load', function (event) {
    layoutId = document.URL.charAt(document.URL.length - 1);
    $('#draggableItems').each(function (index, element) {
        MakeElementsDraggable(element, true);
    });

    livePreviewArea = $('#live-preview-area')[0]
    MakeElementDropTarget(livePreviewArea);
    onClonedElement(function (element) {
        $(element).mouseup(EditOrRemoveTextOnClick);
    });

    var typeSelector = $('#type-selector')[0];
    $(typeSelector).click(
        function (event) {
            layoutTypeSelected = typeSelector.options[typeSelector.selectedIndex].value;
        }
    );

    $(typeSelector).trigger('click');

    console.log(layoutTypeSelected);
});

//Need object on server side to receive json and save the layout
//Don't forget validation of the title field.
function SaveLayout() {

}

//Need to wire up the styling controls.
//Don't forget that it needs to create a script tag and save that as the css and not apply the styles directly to the tags.

