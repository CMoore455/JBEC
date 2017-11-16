
document.addEventListener("load", DocumentLoaded());
var headerId, bodyId, footerId;

function DocumentLoaded() {

    var headerSelector = $(".selector-headers")[0];
    var option = headerSelector.options[headerSelector.selectedIndex];
    headerId = option.value;
    headerSelector.addEventListener("click", function (event) {
        LayoutSelectorChanged({
            ChangeID: function () {
                var option = headerSelector.options[headerSelector.selectedIndex];
                headerId = option.value;
            }
        });
    });

    var bodySelector = $(".selector-bodies")[0];
    var option = bodySelector.options[bodySelector.selectedIndex];
    bodyId = option.value;
    bodySelector.addEventListener("click", function (event) {
        LayoutSelectorChanged({
            ChangeID: function (id) {

                bodyId = bodySelector.options[bodySelector.selectedIndex].value;
            }
        });
    });

    var footerSelector = $(".selector-footers")[0];
    var option = footerSelector.options[footerSelector.selectedIndex];
    footerId = option.value;
    footerSelector.addEventListener("change", function (event) {
        LayoutSelectorChanged({
            ChangeID: function () {
                footerId = footerSelector.options[footerSelector.selectedIndex].value;
            }
        });
    });

};

function LayoutSelectorChanged(IdChanger) {
    IdChanger.ChangeID();
    //Perform ajax request for new layout
    //Apply new layouts to live preview
    //Update the page creation options for a layout.
}

function SavePage() {
    //Navigate DOM to page contents
    //Send page up to server as json
}


//Need function that makes the page creation option for color and sizing etc. appear with appropriate options(TBD)