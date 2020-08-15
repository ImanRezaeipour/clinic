
/**
 *
 * @returns {}
 */
var PrepareDoctorListGrid_OnLoad = function (elem) {
    var columns = [
           {field: "FirstName", title: "نام", width: 200 },
           {field: "LastName",title: "نام خانوادگی",width: 200},
           {field: "MobileNumber",title: "موبایل",width: 200},
           {field: "PhoneNumber",title: "تلفن",width: 200},
           {field: "Shift",title: "شیفت کاری",width: 200},
           {field: "ExpertiseName",title: "تخصص",width: 200}
    ];

    var dataSourceFields = {
        "Id": {type: "string"},
        "FirstName": {type: "string"},
        "LastName": {type: "string"},
        "MobileNumber": {type: "string"},
        "PhoneNumber": {type: "string"},
        "Shift": {type: "string"},
        "ExpertiseName": {type: "string"}

    }



    $(elem).appGrid({
        columns: columns,
        dataSourceUrl: "/doctor/GetListAjax",
        dataSourceFields: dataSourceFields,
        createEventHandler: "doctorListCreate_OnClick",
        editEventHandler: "doctorListEdit_OnClick",
        deleteEventHandler: "doctorListDelete_OnClick"
    });
}

function refreshGrid() {
    $(".k-pager-refresh.k-link").click();
}

var doctorListCreate_OnClick = function (e) {
    window.location.href = '/Doctor/create';
}

var doctorListEdit_OnClick = function (e) {
    var entityGrid = $("#doctorsGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());

    var s = selectedItem.Id;
    window.location.href = '/Doctor/edit/' + selectedItem.Id;
}

var doctorListDelete_OnClick = function (e) {
    var entityGrid = $("#doctorsGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());
    var id = selectedItem.Id;
    if (confirm("آیا از حذف اطمینان دارید?")) {
        $.ajax({
            type: 'POST',
            url: '/Doctor/deleteajax',
            data: { id: id },
            dataType: 'json',
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

/**
 * 
 * @param {} elem 
 * @returns {} 
 */
var prepareExpertiseCombobox_OnLoad = function (elem) {
    var comboboxOptions = {
        dataSourceUrl: "/Expertise/GetSelectListAjax"
    }

    $(elem).appCombobox(comboboxOptions);
}

var prepareDoctorCombobox_OnLoad = function (elem) {
    var comboboxOptions = {
        dataSourceUrl: "/Doctor/GetSelectListAjax"
    }

    $(elem).appCombobox(comboboxOptions);
}
