$(document).on("click", ".loading-shower", function () {
    if ($('.error-search-message') != undefined) $('.error-search-message').hide();
    var theForm = $(this).closest("form");
    if (($(this).data("check-validation") != undefined && !$(this).data("check-validation")) || theForm.valid()) {
        $('.loading-container').removeClass('hidden');
        $('.loading-overlay').removeClass('hidden');
        return true;
    }
    else {
        return false;
    }
});

/*jQuery(function ($) {
            $.datepicker.regional['he'] = {
                closeText: 'סגור',
                prevText: '&#x3C;הקודם',
                nextText: 'הבא&#x3E;',
                currentText: 'היום',
                monthNames: ['ינואר', 'פברואר', 'מרץ', 'אפריל', 'מאי', 'יוני',
                    'יולי', 'אוגוסט', 'ספטמבר', 'אוקטובר', 'נובמבר', 'דצמבר'],
                monthNamesShort: ['ינו', 'פבר', 'מרץ', 'אפר', 'מאי', 'יוני',
                    'יולי', 'אוג', 'ספט', 'אוק', 'נוב', 'דצמ'],
                dayNames: ['ראשון', 'שני', 'שלישי', 'רביעי', 'חמישי', 'שישי', 'שבת'],
                dayNamesShort: ['א\'', 'ב\'', 'ג\'', 'ד\'', 'ה\'', 'ו\'', 'שבת'],
                dayNamesMin: ['א\'', 'ב\'', 'ג\'', 'ד\'', 'ה\'', 'ו\'', 'שבת'],
                weekHeader: 'Wk',
                dateFormat: 'dd/mm/yy',
                firstDay: 0,
                isRTL: true,
                showMonthAfterYear: false,
                yearSuffix: ''
            };
            $.datepicker.setDefaults($.datepicker.regional['he']);

        });
        $(function () {
            $("#student-birth-date").datepicker({
                changeMonth: true,
                changeYear: true
            });

            $("#datepicker").datepicker("option", "dateFormat", "yyyy-mm-dd");
            //$("#datepicker").datepicker("option", $.datepicker.regional["he"]);
        });
        */

// jQuery.extend(jQuery.validator.messages, {
//     required: "This field is required.",
//     remote: "Please fix this field.",
//     email: "Please enter a valid email address.",
//     url: "Please enter a valid URL.",
//     date: "Please enter a valid date.",
//     dateISO: "Please enter a valid date (ISO).",
//     number: "Please enter a valid number.",
//     digits: "Please enter only digits.",
//     creditcard: "Please enter a valid credit card number.",
//     equalTo: "Please enter the same value again.",
//     accept: "Please enter a value with a valid extension.",
//     maxlength: jQuery.validator.format("Please enter no more than {0} characters."),
//     minlength: jQuery.validator.format("Please enter at least {0} characters."),
//     rangelength: jQuery.validator.format("Please enter a value between {0} and {1} characters long."),
//     range: jQuery.validator.format("Please enter a value between {0} and {1}."),
//     max: jQuery.validator.format("Please enter a value less than or equal to {0}."),
//     min: jQuery.validator.format("Please enter a value greater than or equal to {0}.")
// });