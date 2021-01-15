"use strict";
function downloadFromUrl(options) {
    var _a;
    var anchorElement = document.createElement('a');
    anchorElement.href = options.url;
    anchorElement.download = (_a = options.fileName) !== null && _a !== void 0 ? _a : '';
    anchorElement.click();
    anchorElement.remove();
}
function downloadFromByteArray(options) {
    // The byte array in .NET is encoded to base64 string when it passes to JavaScript.
    // So we can pass that base64 encoded string to the browser as a "data URL" directly.
    var url = "data:" + options.contentType + ";base64," + options.byteArray;
    console.log(url);
    downloadFromUrl({ url: url, fileName: options.fileName });
}
function downloadZip() {
    fetch('https://jsonplaceholder.typicode.com/todos/1')
        .then(function (response) {
        if (response.status != 200) {
            var errorMessage = "Error processing the request... (" + response.status + " " + response.statusText + ")";
            throw new Error(errorMessage);
        }
        else {
            return response.blob();
        }
    })
        .then(function (blob) {
        // !!! see next code block !!!
        console.log(blob);
        downloadData("Distance Calculator.zip", blob);
    })
        .catch(function (error) {
        console.error(error);
    });
}
// Solution for big files (source: https://stackoverflow.com/a/25975345/831138)
function downloadData(filenameForDownload, data) {
    var textUrl = URL.createObjectURL(data);
    var element = document.createElement('a');
    console.log(data.text);
    element.setAttribute('href', textUrl);
    element.setAttribute('download', filenameForDownload);
    element.style.display = 'none';
    document.body.appendChild(element);
    element.click();
    document.body.removeChild(element);
}
function getImageSize() {
    var img2 = document.getElementById("uploaded_image");
    if (img2 != null)
        return [img2.clientHeight, img2.clientHeight];
    else
        return [-1, -1];
}
