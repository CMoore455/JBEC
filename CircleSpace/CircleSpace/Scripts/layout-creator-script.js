var layoutTypeSelected, layoutId, currentElementSelected;

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
                currentElementSelected = event.target;
                UpdateCreatorOptions();
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
    $('#fontColor').on('click', ChangeFontColor);
    $('#backgroundColor').on('click', ChangeBackgroundColor);
    $('#marginLeft').on('click', ChangeLeftMargin);
    $('#marginRight').on('click', ChangeRightMargin);
    $('#marginTop').on('click', ChangeTopMargin);
    $('#marginBottom').on('click', ChangeBottomMargin);
    $('#width').on('click', ChangeWidth);
    $('#height').on('click', ChangeHeight);
    $('#fontSize').on('click', ChangeFontSize);
    $('#fontWeight').on('click', ChangeFontWeight);
    $('#fontStyle').on('click', ChangeFontStyle);
    $('#textAlign').on('click', ChangeAlignment);
    $('#check').on('click', ChangeVerticalAlignment);
    $('#submitButton').on('click', CreateLink);


    console.log(layoutTypeSelected);
});

//Need object on server side to receive json and save the layout
//Don't forget validation of the title field.
function SaveLayout() {

}

//Need to wire up the styling controls.
//Don't forget that it needs to create a script tag and save that as the css and not apply the styles directly to the tags.

function UpdateCreatorOptions() {
    document.getElementById('marginLeft').value = currentElementSelected.style.marginLeft.slice(0, currentElementSelected.style.marginTop.length - 2);
    document.getElementById('marginRight').value = currentElementSelected.style.marginRight.slice(0, currentElementSelected.style.marginRight.length - 2);
    document.getElementById('marginTop').value = currentElementSelected.style.marginTop.slice(0, currentElementSelected.style.marginTop.length - 2);
    document.getElementById('marginBottom').value = currentElementSelected.style.marginBottom.slice(0, currentElementSelected.style.marginBottom.length - 2);
    document.getElementById('height').value = currentElementSelected.style.height.slice(0, currentElementSelected.style.height.length - 2);
    document.getElementById('width').value = currentElementSelected.style.width.slice(0, currentElementSelected.style.width.length - 2);
    document.getElementById('fontSize').value = currentElementSelected.style.fontSize.slice(0, currentElementSelected.style.fontSize.length - 2);
    document.getElementById('fontWeight').value = currentElementSelected.style.fontWeight.slice(0, currentElementSelected.style.fontWeight.length);
    document.getElementById('backgroundColor').value = rgbToHex();
    document.getElementById('fontColor').value = rgbToHex();
    document.getElementById('textAlign').value = currentElementSelected.style.textAlign != "" ? currentElementSelected.style.textAlign : "left";
    document.getElementById('fontStyle').value = currentElementSelected.style.fontStyle != "" ? currentElementSelected.style.fontStyle : "normal";
    document.getElementById('check').checked = currentElementSelected.style.lineHeight == currentElementSelected.style.height
    document.getElementById('textArea').value = "";
}

function ChangeLeftMargin(margin) {
    BlurCurrentlySelectedElement();
    jQuery('#marginLeft').on('input', function () {
        jQuery(currentElementSelected).css('marginLeft', (jQuery(this).val() + 'px'));
    });
}

function ChangeRightMargin() {
    BlurCurrentlySelectedElement();
    jQuery('#marginRight').on('input', function () {
        jQuery(currentElementSelected).css('marginRight', (jQuery(this).val() + 'px'));
    });
}

function ChangeTopMargin() {
    BlurCurrentlySelectedElement();
    jQuery('#marginTop').on('input', function () {
        jQuery(currentElementSelected).css('marginTop', (jQuery(this).val() + 'px'));
    });
}

function ChangeBottomMargin() {
    BlurCurrentlySelectedElement();
    jQuery('#marginBottom').on('input', function () {
        jQuery(currentElementSelected).css('marginBottom', (jQuery(this).val() + 'px'));
    });
}

function ChangeHeight() {
    BlurCurrentlySelectedElement();
    jQuery('#height').on('input', function () {
        jQuery(currentElementSelected).css('height', jQuery(this).val());
    });
}

function ChangeWidth() {
    BlurCurrentlySelectedElement();
    jQuery('#width').on('input', function () {
        jQuery(currentElementSelected).css('width', jQuery(this).val());
    });
}

function ChangeBackgroundColor() {
    BlurCurrentlySelectedElement();
    jQuery('#backgroundColor').on('input', function () {
        jQuery(currentElementSelected).css('backgroundColor', jQuery(this).val());
    });
}

function ChangeFontColor() {
    BlurCurrentlySelectedElement();
    jQuery('#fontColor').on('input', function () {
        jQuery(currentElementSelected).css('color', jQuery(this).val());
    });
}

function ChangeFontSize() {
    BlurCurrentlySelectedElement();
    jQuery('#fontSize').on('input', function () {
        jQuery(currentElementSelected).css('fontSize', (jQuery(this).val() + 'px'));
    });
}

function ChangeFontWeight() {
    BlurCurrentlySelectedElement();
    jQuery('#fontWeight').on('input', function () {
        jQuery(currentElementSelected).css('fontWeight', jQuery(this).val());
    });
}

function ChangeFontStyle() {
    BlurCurrentlySelectedElement();
    jQuery('#fontStyle').on('input', function () {
        jQuery(currentElementSelected).css('font-style', jQuery(this).val());
    });
}

function ChangeAlignment() {
    BlurCurrentlySelectedElement();
    jQuery('#textAlign').on('input', function () {
        jQuery(currentElementSelected).css('textAlign', jQuery(this).val());
    });
}

function ChangeVerticalAlignment() {
    BlurCurrentlySelectedElement();
    if (document.getElementById("check").checked == true) {
        jQuery('#check').on('input', function () {
            jQuery(currentElementSelected).css('lineHeight', currentElementSelected.style.height);
        });
    }
    else {
        jQuery('#check').on('input', function () {
            jQuery(currentElementSelected).css('lineHeight', (40 + 'px'));
        });
    }
}

function CreateLink() {
    if (document.getElementById('textArea').value != "") {
        $(currentElementSelected).wrap("<a href=" + document.getElementById('textArea').value + "></a>");
    }
    else {
        $(currentElementSelected).unwrap();
    }
}

function BlurCurrentlySelectedElement() {
    $(currentElementSelected).children().trigger('blur');
}

function rgbToHex() {
    var a = currentElementSelected.style.backgroundColor.slice(4, currentElementSelected.style.backgroundColor.length - 1);
    a = a.split(",");
    var b = a.map(function (x) {             //For each array element
        x = parseInt(x).toString(16);      //Convert to a base16 string
        return (x.length == 1) ? "0" + x : x;  //Add zero if we get only one character
    })
    b = "#" + b.join("");
    return b;
}
