//头部模块固定
(function fixHeader() {
    const header = document.querySelector('.header')
    const nav = document.querySelector('.nav')
    window.addEventListener('scroll', function () {
        // 固定头部模块
        if (document.documentElement.scrollTop >= header.offsetTop + header.offsetHeight + 20) {
            nav.classList.add('nav-fix')
            header.classList.add('header-fix')
        } else {
            nav.classList.remove('nav-fix')
            header.classList.remove('header-fix')
        }
    })
})();