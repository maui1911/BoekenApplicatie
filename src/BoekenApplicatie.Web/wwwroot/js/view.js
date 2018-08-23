function changeToPage(pageNumber) {
    document.getElementById("SortFilterPageData_CurrentPage").value = pageNumber;
    document.getElementById("viewForm").submit();
};

function sortBy(propName) {
    var desc = document.getElementById("SortFilterPageData_OrderByDesc").value === 'true';

    if (document.getElementById("SortFilterPageData_OrderByProp").value === propName) {
        document.getElementById("SortFilterPageData_OrderByDesc").value = !desc;
    } else {
        document.getElementById("SortFilterPageData_OrderByProp").value = propName;
        document.getElementById("SortFilterPageData_OrderByDesc").value = false;
    };

    document.getElementById("viewForm").submit();
};