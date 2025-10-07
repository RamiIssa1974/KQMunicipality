$(document).ready(function () {
    console.log("ready!");
    $("select#teacher1_list,select#teacher2_list").find("option").each(function () {
        if ($(this).data("school-id") == -1) {
            $(this).prop("disabled", false);
            $(this).show();
        } else {
            $(this).prop("disabled", true);
            $(this).hide();
        }
    });

    $(document).on("input", "#school1_list", function () {
        toggleLoading(true);
        var selectedSchoolId = $(this).val();
        $("#teacher1_list").val(-1);
        $("#teacher1_list").find("option").each(function () {
            if ($(this).data("school-id") == -1 || $(this).data("school-id") == selectedSchoolId) {
                $(this).prop("disabled", false);
                $(this).show();
            } else {
                $(this).prop("disabled", true);
                $(this).hide();

            }
        });

        sleep(1000).then(() => {
            toggleLoading(false);
        });

    });
    $(document).on("input", "#school2_list", function () {
        toggleLoading(true);
        var selectedSchoolId = $(this).val();
        $("#teacher2_list").val(-1);
        $("#teacher2_list").find("option").each(function () {
            if ($(this).data("school-id") == -1 || $(this).data("school-id") == selectedSchoolId) {
                $(this).prop("disabled", false);
                $(this).show();
            } else {
                $(this).prop("disabled", true);
                $(this).hide();

            }
        });
        sleep(1000).then(() => {
            toggleLoading(false);
        });
    });


    var reject_form = $('#reject-form');
    var error_roles_form = $('.validation-errors-container', reject_form);

    jQuery.validator.setDefaults({
        debug: false,// set to true to avoid form submit
        success: "valid",
    });
    //teacher1_list,#teacher2_list,#school1_list,#school2_list
    $("#reject-form").validate({
        errorLabelContainer: error_roles_form,//shows one validation error message for all the validation messages
        onkeyup: false,
        onclick: false,
        onfocusout: false,
        errorPlacement: function (error, element) {/*prevent errors for each element */ },
        rules: {
            "rejectData.SelectedSchoolId1": { required: true, min: 0 },
            "rejectData.SelectedSchoolId1": { required: true, min: 0 },
            "rejectData.SelectedTeacherId1": { required: true, min: 0 },
            "rejectData.SelectedTeacherId2": { required: true, min: 0 },
            "rejectData.Reason": { required: true, maxlength:255 }
        },
        messages: {
            "rejectData.SelectedSchoolId1": "اختر مدرسة الافضليه الاولى.",
            "rejectData.SelectedSchoolId2": "اختر مدرسة الافضليه الثانيه.",
            "rejectData.SelectedTeacherId1": "اختر معلمة الافضليه الاولى.",
            "rejectData.SelectedTeacherId2": "اختر معلمة الافضليه الثانيه.",
            "rejectData.Reason": {
                required: "املا خانة اسباب الرفض.",
                maxlength: "The ادخل حتى 255 حرف."
            }
        }
    });

    $("#teacher1_list").find("option").each(function () {
        if ($(this).val() == -1) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
    $("#teacher2_list").find("option").each(function () {
        if ($(this).val() == -1) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });

    function sleep(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }
    function toggleLoading(isShow) {
        if (isShow) {
            $('.loading-container').removeClass('hidden');
            $('.loading-overlay').removeClass('hidden');
        }
        else {
            $('.loading-container').addClass('hidden');
            $('.loading-overlay').addClass('hidden');
        }
    }
});