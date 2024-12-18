window.CountResize = 0;
window.helper = {

    getInnerWidth: function () {
        return window.innerWidth;
    },
    withContext: {
        resizeCallback: (dotnetHelper) => {
            setTimeout(function () {
                if (window.CountResize > 0) {
                    window.CountResize = 0;
                    dotnetHelper.invokeMethodAsync('OnResize', window.innerWidth, window.innerHeight);
                }
            }, 500)
            window.CountResize += 1
        },
        registerViewportChangeCallback: (dotnetHelper) => {
            window.addEventListener('load', () => window.helper.withContext.resizeCallback(dotnetHelper));
            window.addEventListener('resize', () => window.helper.withContext.resizeCallback(dotnetHelper));
        },
        removeViewportChangeCallback: (dotnetHelper) => {
            window.removeEventListener('resize', () => window.helper.withContext.resizeCallback(dotnetHelper))
            window.removeEventListener('load', () => window.helper.withContext.resizeCallback(dotnetHelper))
        }
    },
    setTheme: function (darkmode) {
        let theme = darkmode ? "dark" : "light";
        document.getElementsByTagName("html")[0].setAttribute("data-bs-theme", theme)
    },
    prefersDarkMode: function () {
        return window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;
    }

}
