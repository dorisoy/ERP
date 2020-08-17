//下拉列表数据 hasPleaseSelect值为1时，有“请选择”
function BindDictItem(control, dictTypeName, hasPleaseSelect) {
    var url = '/Base/GetDictJson?dictTypeName=' + dictTypeName;
    if (undefined != hasPleaseSelect) {
        url += '&hasPleaseSelect=' + hasPleaseSelect;
    }
    $('#' + control).combobox({
        url: url,
        valueField: 'Value',
        textField: 'Text'
    });
}