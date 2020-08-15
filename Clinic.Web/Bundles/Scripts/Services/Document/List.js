var prepareDocumentListGrid_OnLoad = function (elem) {
    var dataSourceFields = {
        Id: { type: "string" },
        FirstName: { type: "string" },
        LastName: { type: "string" },
        CustomCode: { type: "string" },
        DoctorLastName: { type: "string" },
        PresenterLastName: { type: "string" },
        PhoneNumber: { type: "string" },
        MobileNumber: { type: "string" },
        CreatedOn: { type: "string" },
        ModifiedOn: { type: "string" }
    }

    $(elem).appGrid({
        columns: [
          { field: "CustomCode", title: "شماره پرونده", width: 200 },
          { field: "FirstName", title: "نام", width: 200 },
          { field: "LastName", title: "نام خانوادگی", width: 200 },
          { field: "DoctorLastName", title: "نام دکتر", width: 200 },
          { field: "PresenterLastName", title: "نام معرف", width: 200 },
          { field: "PhoneNumber", title: "تلفن", width: 200 },
          { field: "MobileNumber", title: "همراه", width: 200 },
          { field: "CreatedOn", title: "تاریخ ایجاد", width: 200 },
          { field: "ModifiedOn", title: "تاریخ ویرایش", width: 200 }
        ],
        dataSourceUrl: "/Document/GetListAjax",
        dataSourceFields: dataSourceFields,
        createEventHandler: "documentListCreate_OnClick",
        editEventHandler: "documentListEdit_OnClick",
        deleteEventHandler: "documentListDelete_OnClick"
    });
}

var documentListCreate_OnClick = function (e) {
    window.location.href = '/Document/create';
}

var documentListEdit_OnClick = function (e) {
    var entityGrid = $("#DocumentsGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());
    window.location.href = '/Document/edit/' + selectedItem.Id;
}

var documentListDelete_OnClick = function (e) {
    var entityGrid = $("#DocumentsGridContainer").data("kendoGrid");
    var selectedItem = entityGrid.dataItem(entityGrid.select());
    var id = selectedItem.Id;
    if (confirm("آیا از حذف اطمینان دارید?")) {
        $.ajax({
            type: 'POST',
            url: '/Document/deleteajax',
            data: { id: id },
            dataType: 'json',
            complete: function (xhr, status) {
                refreshGrid();
            }
        });
    }
    return false;
}