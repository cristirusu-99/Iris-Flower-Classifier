
function downloadFromUrl(options: { url: string, fileName?: string }): void {
    const anchorElement = document.createElement('a');
    anchorElement.href = options.url;
    anchorElement.download = options.fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
}

function downloadFromByteArray(options: { byteArray: string, fileName: string, contentType: string }): void {
    // The byte array in .NET is encoded to base64 string when it passes to JavaScript.
    // So we can pass that base64 encoded string to the browser as a "data URL" directly.
    const url = "data:" + options.contentType + ";base64," + options.byteArray;
    console.log(url);
    downloadFromUrl({ url: url, fileName: options.fileName });
}

function downloadZip() {
    fetch('https://jsonplaceholder.typicode.com/todos/1')
        .then((response) => {
            if (response.status != 200) {
                let errorMessage = "Error processing the request... (" + response.status + " " + response.statusText + ")";
                throw new Error(errorMessage);
            } else {
                return response.blob();
            }
        })
        .then((blob: any) => {
            // !!! see next code block !!!
            console.log(blob);
            downloadData("Distance Calculator.zip", blob);
        })
        .catch(error => {
            console.error(error);
        });
}

// Solution for big files (source: https://stackoverflow.com/a/25975345/831138)
function downloadData(filenameForDownload: string, data: any) {
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