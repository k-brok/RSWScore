window.downloadFile = (base64, fileName) => {
    const link = document.createElement('a');
    link.href = base64;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};
window.convertUtcToLocal = (utcDate) => {
    return new Date(utcDate + 'Z').toISOString().slice(0, 16); // Formatteer naar 'YYYY-MM-DDTHH:mm'
};

window.convertLocalToUtc = (localDate) => {
    let date = new Date(localDate);
    return date.toISOString().slice(0, 16); // Formatteer naar 'YYYY-MM-DDTHH:mm'
};
