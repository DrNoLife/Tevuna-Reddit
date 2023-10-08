// wwwroot/scroller.js
window.scrollToElement = (elementId) => {

    var element = document.getElementById(elementId);

    if (element) {
        element.scrollIntoView({ behavior: 'smooth' });
    } 
}
