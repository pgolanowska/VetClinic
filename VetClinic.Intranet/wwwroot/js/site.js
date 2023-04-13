var toggler = document.getElementsByClassName("sidebar-expandable");
var i;

for (i = 0; i < toggler.length; i++) {
    toggler[i].addEventListener("click", function () {
        this.parentElement.querySelector(".sidebar-nested").classList.toggle("active");
        this.classList.toggle("sidebar-expanded");
    });
}