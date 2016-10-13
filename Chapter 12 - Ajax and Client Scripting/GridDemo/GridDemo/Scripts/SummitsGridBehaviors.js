$(function() {
    initializeTable();

    $("<label><input id='heights' type='checkbox' checked='true'/>Show heights</label>")
            .insertBefore("#summits")
            .children("input").click(function() {
                $("#summits td:nth-child(2)").toggle();
            }).click();

    $("#summits A").live("click", function() {
        $.get($(this).attr("href"), function(response) {
            $("#summits").replaceWith($("#summits", response));

            initializeTable();

            // Respect the (un)checked state of the "show heights" check box
            if (!$("#heights")[0].checked)
                $("#summits td:nth-child(2)").hide();
        });
        return false;
    });
});

function initializeTable() {
    // Zebra striping
    $("#summits tr:even").addClass("alternate");

    // Deletion confirmations
    $("#summits form[action$='/DeleteItem']").submit(function() {
        var itemText = $("input[name='item']", this).val();
        return confirm("Are you sure you want to delete '" + itemText + "'?");
    });
}