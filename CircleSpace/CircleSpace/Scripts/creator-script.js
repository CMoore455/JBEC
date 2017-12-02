
document.addEventListener("load", DocumentLoaded());

//The ID's of the currently selected layouts in the selector.
var headerId, bodyId, footerId;
var currentElementSelected;


//Hooks up all the click events for the selectors for a different layout.
function DocumentLoaded() {
    var headerClassLookup = $(".selector-headers");
    var headerSelector = headerClassLookup[0];
    var option = headerSelector.options[headerSelector.selectedIndex];
    if (option) {
        headerId = option.value;
        headerClassLookup.click(function (event) {
            LayoutSelectorChanged({
                Type: "Header",
                ChangeID: function () {
                    var option = headerSelector.options[headerSelector.selectedIndex];
                    headerId = option.value;
                }
            });
        });
        headerClassLookup.trigger('click');
    }

    var bodyClassLookup = $(".selector-bodies");
    var bodySelector = bodyClassLookup[0];
    var option = bodySelector.options[bodySelector.selectedIndex];
    if (option) {
        bodyId = option.value;
        bodyClassLookup.click(function (event) {
            LayoutSelectorChanged({
                Type: "Body",
                ChangeID: function (id) {

                    bodyId = bodySelector.options[bodySelector.selectedIndex].value;
                }
            });
        });
        bodyClassLookup.trigger('click');
    }

    var footerClassLookup = $(".selector-footers")
    var footerSelector = footerClassLookup[0];
    var option = footerSelector.options[footerSelector.selectedIndex];
    if (option) {
        footerId = option.value;
        footerClassLookup.click(function (event) {
            LayoutSelectorChanged({
                Type: "Footer",
                ChangeID: function () {
                    footerId = footerSelector.options[footerSelector.selectedIndex].value;
                }
            });
        });
        footerClassLookup.trigger('click');
    }

    $('#fontColor').on('click', ChangeFontColor);
    $('#backgroundColor').on('click', ChangeBackgroundColor);
    $('#marginLeft').on('click', ChangeLeftMargin);
    $('#marginRight').on('click', ChangeRightMargin);
    $('#marginTop').on('click', ChangeTopMargin);
    $('#marginBottom').on('click', ChangeBottomMargin);
    $('#width').on('click', ChangeWidth);
    $('#height').on('click', ChangeHeight);
    $('.fontSize').on('click', ChangeFontSize);
    $('.fontStyle').on('click', ChangeFontStyle);
    $('.fontWeight').on('click', ChangeFontWeight);
    $('.textAlign').on('click', ChangeAlignment);
    $('#check').on('click', ChangeVerticalAlignment);
};


//Performs ajax request for the new layouts. Updates to the live preview area occur on received response.
function LayoutSelectorChanged(IdChanger) {
    IdChanger.ChangeID();
    //Perform ajax request for new layout
    switch (IdChanger.Type) {
        case "Header":
            $.getJSON("GetNewLayout/" + headerId).done(ReceiveNewLayout);
            break;
        case "Body":
            $.getJSON("GetNewLayout/" + bodyId).done(ReceiveNewLayout);
            break;
        case "Footer":
            $.getJSON("GetNewLayout/" + footerId).done(ReceiveNewLayout);
            break;
        default:
            return;
    }

    //Update the page creation options for a layout.
}

//Responds to json received from server when new layouts are requested.
function ReceiveNewLayout(json) {
    switch (json.Type) {
        case "Header":
            ChangeContent(json.Content, json.CSS, '#headerPreview');
            break;
        case "Body":
            ChangeContent(json.Content, json.CSS, '#bodyPreview');
            break;
        case "Footer":
            ChangeContent(json.Content, json.CSS, '#footerPreview');
            break;
        default:
            break;
    }
}


//These Change/Header|Body|Footer/ methods update the live preview area when a click occurs on the selection control.
function ChangeContent(content, css, idSelectorForContentPlacement) {
    var previewArea = $(idSelectorForContentPlacement)[0];

    if ($(idSelectorForContentPlacement + 'Style').length != 0) { $(idSelectorForContentPlacement + 'Style').remove(); }

    var cssStyle = document.createElement('style');
    cssStyle.setAttribute('id', idSelectorForContentPlacement + 'Style');
    cssStyle.setAttribute('type', 'text/css');

    for (var i = 0; i < css.length; i++) {
        cssStyle.innerText += ' ' + idSelectorForContentPlacement + ' ' + css[i];
    }

    $('head').append(cssStyle);

    var newHeader = document.createElement("div");
    newHeader.innerHTML = content;
    previewArea.innerHTML = '';
    while (newHeader.hasChildNodes()) {
        previewArea.appendChild(newHeader.childNodes[0]);
    }

    WireContentForEditableText(previewArea);
}
//Sends the page to the server to be saved to the database.
function SavePage() {

    //Creates header, body, and footer tags to send to the server.
    var header = document.createElement('header');
    header.innerHTML = $("#headerPreview")[0].innerHTML;
    var body = document.createElement('body');
    body.innerHTML = $("#bodyPreview")[0].innerHTML;
    var footer = document.createElement('footer');
    footer.innerHTML = $("#footerPreview")[0].innerHTML;

    //Finding the style tags of the live preview areas contents.
    var headerCssRules = $("#headerPreviewStyle");
    var bodyCssRules = $("#bodyPreviewStyle");
    var footerCssRules = $("#footerPreviewStyle");

    //An array of the style tags applying to the live preview areas contents
    var cssRulesOfPreviewAreas = [];

    //If these exist on the page they will be added to the array that is used to populate the css field of the page.
    if (headerCssRules.length > 0) { cssRulesOfPreviewAreas.push(headerCssRules[0]); }
    if (bodyCssRules.length > 0) { cssRulesOfPreviewAreas.push(bodyCssRules[0]); }
    if (footerCssRules.length > 0) { cssRulesOfPreviewAreas.push(footerCssRules[0]); }


    var cssRules = ConvertAllRulesToOneString(cssRulesOfPreviewAreas);

    //This objects fields needs to match JSONForSavingWebPage object in the models folder of server.
    var page = {
        Header: header.outerHTML,
        Body: body.outerHTML,
        Footer: footer.outerHTML,
        CSS: cssRules
        //Need ImageURLS
        //Need RouteOfPage
    }

    //Sending page to server as JSON
    $.post("SavePage", page);//Need to react to bad post (i.e. Route is already taken)
}

//Converts all the css in several style tags into one blob of css text.
function ConvertAllRulesToOneString(arrayOfStyles) {
    var cssRulesTogetherAsString = "";
    while (arrayOfStyles.length > 0) {
        var cssRulesAsArray = SeparateCSSRulesIntoArray(arrayOfStyles[0].innerText);
        for (var i = 0; i < cssRulesAsArray.length; i++) {
            cssRulesTogetherAsString += cssRulesAsArray[0].trim() + ' ';
        }
        arrayOfStyles.shift();
    }
    return cssRulesTogetherAsString;
}

//Separates out each css rule of a blob of css into an array.
function SeparateCSSRulesIntoArray(cssText) {
    var matchCssRulesRegex = /[\)\(\]\[:\w," =\-\*^#\.\@>\n\+]+\{\s*[^}{]+\s*\}/;
    return cssText.match(matchCssRulesRegex);
}


//Finds nodes with 0 children and makes their text editable
function WireContentForEditableText(content) {
    if (content.children.length != 0) {
        for (var i = 0; i < content.children.length; i++) {
            WireContentForEditableText(content.children[i]);
        }
    } else if (content != undefined) {
        var changeTextClickSubscriber;
        $(content).click(function (event) {
            ChangeTextClickSubscriber(content);
        });
    }
}

function ChangeTextClickSubscriber(content) {

    if (content.children.length == 0) {
        var inputTag = document.createElement('input');
        inputTag.type = 'text';
        inputTag.value = content.innerText;
        content.innerText = '';
        $(inputTag).blur(function (event) {
            var inputTagChild = content.childNodes[0];
            content.removeChild(inputTagChild);
            content.innerText = inputTagChild.value;
            $(content).off();
            $(content).click(function (event) { ChangeTextClickSubscriber(content) });
        });
        content.appendChild(inputTag);
        currentElementSelected = content;
        UpdateCreatorOptions();
    }

}

//Need function that makes the page creation option for color and sizing etc. appear with appropriate options(TBD)
function UpdateCreatorOptions() {
    document.getElementById('marginLeft').value = currentElementSelected.style.marginLeft.slice(0, currentElementSelected.style.marginTop.length - 2);
    document.getElementById('marginRight').value = currentElementSelected.style.marginRight.slice(0, currentElementSelected.style.marginRight.length - 2);
    document.getElementById('marginTop').value = currentElementSelected.style.marginTop.slice(0, currentElementSelected.style.marginTop.length - 2);
    document.getElementById('marginBottom').value = currentElementSelected.style.marginBottom.slice(0, currentElementSelected.style.marginBottom.length - 2);
    document.getElementById('backgroundColor').value = rgbToHex();
    document.getElementById('fontColor').value = rgbToHex();
}

function rgbToHex() {
    var a = currentElementSelected.style.backgroundColor.slice(4, currentElementSelected.style.backgroundColor.length-1);
    a = a.split(",");
    var b = a.map(function (x) {             //For each array element
        x = parseInt(x).toString(16);      //Convert to a base16 string
        return (x.length == 1) ? "0" + x : x;  //Add zero if we get only one character
    })
    b = "#" + b.join("");
    return b;
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
    jQuery('.fontSize').on('input', function () {
        jQuery(currentElementSelected).css('fontSize', jQuery(this).val());
    });
}

function ChangeFontStyle() {
    BlurCurrentlySelectedElement();
    jQuery('.fontStyle').on('input', function () {
        jQuery(currentElementSelected).css('fontStyle', jQuery(this).val());
    });
}

function ChangeFontWeight() {
    BlurCurrentlySelectedElement();
    jQuery('.fontWeight').on('input', function () {
        jQuery(currentElementSelected).css('fontWeight', jQuery(this).val());
    });
}

function ChangeAlignment() {
    BlurCurrentlySelectedElement();
    jQuery('.textAlign').on('input', function () {
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

function BlurCurrentlySelectedElement() {
    $(currentElementSelected).children().trigger('blur');
}