$(document).ready(function() {
    $("#addAssertion_TagName, #tagName").autocomplete("/Tag/StartingWith", { autoFill: true, minChars: 2, width: 287 });
});