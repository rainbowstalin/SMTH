﻿function submitReport(url, reportData) {
    console.log(url, reportData);
    $.ajax({
        type: "POST",
        url: url,
        data: reportData,
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            console.log(data);
        }
    });
}