/*
    Credits to Hyperplexed for this code.
    https://www.youtube.com/watch?v=5a8NyGLlorI
*/

window.skewImage = {
    applySkew: function (elementId) {
        const logo = document.getElementById(elementId);
        const images = logo.querySelectorAll("img");

        const shift = (image, index, rangeX, rangeY) => {
            const translationIntensity = 4,
                maxTranslation = translationIntensity * (index + 1),
                currentTranslation = `${maxTranslation * rangeX}% ${maxTranslation * rangeY}%`;

            image.animate({
                translate: currentTranslation,
            }, { duration: 750, fill: "forwards", easing: "ease" });
        }

        const shiftAll = (images, rangeX, rangeY) =>
            images.forEach((image, index) => shift(image, index, rangeX, rangeY));

        const shiftLogo = (e, images) => {
            const rect = logo.getBoundingClientRect(),
                radius = 1000;

            const centerX = rect.left + (rect.width / 2),
                centerY = rect.top + (rect.height / 2);

            const rangeX = (e.clientX - centerX) / radius,
                rangeY = (e.clientY - centerY) / radius;

            shiftAll(images, rangeX, rangeY);
        }

        const resetLogo = () => shiftAll(images, 0.4, -0.7);

        window.onmousemove = e => shiftLogo(e, images);

        document.body.onmouseleave = () => resetLogo();

        resetLogo();
    }
};
