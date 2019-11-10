function highlightTableRow(idOfControlThatWasClicked, classToApply) {
    // The checkbox in <THEAD> toggles all other checkboxs in the table and should therefore not 
    // trigger setting the table row to appear selected.
    if (!(idOfControlThatWasClicked == 'chkToggleAll')) {
        var parts = idOfControlThatWasClicked.split('.');
        var rowID = 'tblRow.' + parts[1];
        var row = document.getElementById(rowID);
        row.className = classToApply;
    }
}

function NotApplicableClicked(rowNo, isChecked) {
    // IMPORTANT!!! jQuery has issues with letting you select elements with a . (period) in their ID.
    // To get around this issue you have to use two backslashes before each special character. A backslash 
    // in a jQuery selector escapes the next character. But you need two of them because backslash is also 
    // the escape character for JavaScript strings. The first backslash escapes the second one, giving you 
    // one actual backslash in your string - which then escapes the next character for jQuery.
    // See http://stackoverflow.com/questions/350292/how-do-i-get-jquery-to-select-elements-with-a-period-in-their-id

    var isDisabled = false;
    var numerator = '';
    var denominator = '1';

    if (isChecked) {
        isDisabled = true;
        numerator = '0';
        denominator = '0';
    }

    $('#txtNumerator\\.' + rowNo).attr('disabled', isDisabled);
    $('#txtNumerator\\.' + rowNo).val(numerator);

    $('#txtDenominator\\.' + rowNo).attr('disabled', isDisabled);
    $('#txtDenominator\\.' + rowNo).val(denominator);

    $('#btnDetails\\.'     + rowNo).attr('disabled', isDisabled);
}

function SaveObservation(isMarkAsCompleted) {
    alert('Save changes to database');
    alert('Perform row validation logic and set error messages where appropriate');
}

function MarkObservationAsCompleted() {
    alert('perform row by row validation');
    alert('Perform logic in SaveObservation(true) function.');
    alert('If no errors, go somewhere.');
}

$(document).ready(function () {

    // Assign number of questions in tracer to a hidden form field.
    $("#NoOfQuestions").val($("#tracerQuestions >tbody > tr").length);

    // 1. When user clicks checkbox, get row number & determine if they checked the box 'on' or 'off'.
    // 2. Call method to disable textboxes and set compliant/non-compliant to 'N/A'.
    $("input:checkbox[id*='chkNotApplicable']").click(function () {
        var idParts = $(this).attr('id').split('.');
        var rowAccessed = idParts[1];
        var isChecked = false;
        if ($(this).is(':checked')) {
            isChecked = true;
        }
        NotApplicableClicked(rowAccessed, isChecked);
    });

    // When specific checkbox is clicked, check all other checkboxes in the selected table.
    $('#chkToggleAll').click(function () {
        var checkboxes = $('td input:checkbox[id^="chkNotApplicable"]');
        var isChecked = true;
        if ($(this).is(':checked')) {
            checkboxes.prop('checked', 'checked');
        } else {
            checkboxes.prop('checked', '');
            isChecked = false;
        }

        var rowsInTable = $("#tracerQuestions >tbody > tr").length;
        for (var i = 1; i <= rowsInTable; i++) {
            NotApplicableClicked(i, isChecked);
        }
    });

    // Dynamically app or remove CSS class on table row for the hover event. This snippet excludes any
    // changes to the <THEAD> row and apply the change to those rows inside the <TBODY> element.
    $('#tracerQuestions > tbody > tr:not(:has(table, th))').hover(function () {
        $(this).addClass('hover');
    }, function () {
        $(this).removeClass('hover');
    });

    $('#tracerQuestions > tbody > tr:not(:has(table, th))').click(function () {
        $(this).removeClass('hover');
        $('#tracerQuestions tr.selectedRow').removeClass('selectedRow');
        $(this).addClass('selectedRow');
    });

    // Set class for current row whenever user clicks on a textbox or checkbox.
    $("input:text").focus(function () {
        // Select all text when any textbox has focus.
        $(this).select();

        // Find all rows that were previously selected and unselect them.
        $('#tracerQuestions tr.selectedRow').removeClass('selectedRow');
        highlightTableRow((this).name, 'selectedRow');
    });
});