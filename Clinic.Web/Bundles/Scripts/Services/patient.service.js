var PreparePatientListGrid_OnLoad = function (elem) {
    var columns = [
        { field: "FirstName", title: "نام", width: 200 },
        { field: "LastName", title: "نام خانوادگی", width: 200 },
        { field: "FatherName", title: "نام پدر", width: 200 },
        { field: "CertificateCode", title: "شماره شناسنامه", width: 200 },
        { field: "NationalCode", title: "شماره ملی", width: 200 },
        { field: "BirthDayOn", title: "تاریخ تولد", width: 200 },
        { field: "BirthDayPlace", title: "محل تولد", width: 200 },
        { field: "MobileNumber", title: "موبایل", width: 200 },
        { field: "PhoneNumber", title: "تلفن", width: 200 },
        { field: "CreatedOn", title: "تاریخ ایجاد", width: 200 }
    ];

    var dataSourceFields = {
        "Id": { type: "string" },
        "FirstName": { type: "string" },
        "LastName": { type: "string" },
        "FatherName": { type: "string" },
        "PhoneNumber": { type: "string" },
        "MobileNumber": { type: "string" },
        "BirthDayOn": { type: "string" },
        "BirthDayPlace": { type: "string" },
        "CertificateCode": { type: "string" },
        "NationalCode": { type: "string" },
        "CreatedOn": { type: "string" }
    }

    $("#patientsGridContainer").appGrid({
        columns: columns,
        dataSourceUrl: "/patient/getlistajax",
        dataSourceFields: dataSourceFields,
        createEventHandler: "patientListNew_OnClick",
        editEventHandler: "patientListEdit_OnClick",
        deleteEventHandler: "patientListDelete_OnClick"
    });
}

function refreshGrid() {
    $(".k-pager-refresh.k-link").click();
}

var patientListNew_OnClick = function (e) {
    window.location.href = "/patient/create";
}

var patientListEdit_OnClick = function (e) {
    var entityGrid = $("#patientsGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());

    window.location.href = "/patient/edit/" + selectedItem.NationalCode;
}

var patientListDelete_OnClick = function (e) {
    var entityGrid = $("#patientsGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());
    var id = selectedItem.Id;
    if (confirm("آیا از حذف اطمینان دارید?")) {
        $.ajax({
            type: "POST",
            url: "/patient/deleteajax",
            data: { id: id },
            dataType: "json",
            complete: function (xhr, status) {
                refreshGrid();
            }
        });
    }
    return false;
}

var multipelid = function () {
    var entityGrid = $("#EntitesGrid").data("kendoGrid");
    var rows = entityGrid.select();
    rows.each(function (index, row) {
        var selectedItem = entityGrid.dataItem(row);
        // selectedItem has EntityVersionId and the rest of your model
    });
}

var validateNewPatientForm = function () {
    //$("#newPatient").simpleValidate();
}

var fillMaskedInput = function () {
    $("#BirthDayOn").kendoMaskedTextBox({
        mask: "1300/00/00"
    });
}

var isExistNationalCode = function () {
    var nationalCode = $('#NationalCode').val();
    $.ajax({
        url: '/patient/IsExistNationalCodeAjax',
        data: { nationalCode: nationalCode },
        dataType: "json",
        type: "GET",
        complete: function (xhr, status) {
            console.log(xhr);
            if (xhr.responseJSON.Data === true) {
                window.location.href = "/patient/edit/" + nationalCode;
            };
        }
    });
}

var prepareDocumentPatientCombobox_OnLoad = function (elem) {
    var comboboxOptions = {
        dataSourceUrl: "/Patient/GetSelectListAjax"
    }

    $(elem).appCombobox(comboboxOptions);
}