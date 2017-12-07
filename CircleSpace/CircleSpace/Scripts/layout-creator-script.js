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

    return false;
}

window.addEventListener('load', function (event) {
    layoutId = document.URL.charAt(document.URL.length - 1);
    $('#draggableItems').each(function (index, element) {
        MakeElementsDraggable(element, true);
    });

    livePreviewArea = $('#live-preview-area')[0];
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

    $('#live-preview-area').children('h1, h2, h3, h4, h5, h6, p, ul, ol, li, a').mouseup(EditOrRemoveTextOnClick);

    $('#live-preview-area > a').click(function () { return false; });

    $(typeSelector).click();

});

//Need object on server side to receive json and save the layout
//Don't forget validation of the title field.
function SaveLayout() {
    /*
              LayoutTitle = o.Title,
                Content = o.Content,
                ID = o.ID,
                OwnerID = User.Identity.GetUserId(),
                CSS = o.CSS,
                Tags = o.Tags,
                Type = o.Type
    */

    layout = {
        Title: $('#layoutTitle')[0].innerText,
        Content: $('#live-preview-area')[0].innerHTML,
        ID: layoutId,
        CSS: $('css-for-layout')[0].innerText,
        Tags: [],
        Type: layoutTypeSelected
    }

    $.post('/Creator/SaveLayout', layout).fail(function () {

    }).done(function () {

    });
}

//Need to wire up the styling controls.
//Don't forget that it needs to create a script tag and save that as the css and not apply the styles directly to the tags.

