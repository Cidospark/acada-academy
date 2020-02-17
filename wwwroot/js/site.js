//site.js where all the good stuff starts

////Vanilla Javascript style
//(function () {
//    var uname = document.getElementById("username");
//    uname.innerHTML = "Bobryska Idogovan";

//    var main = document.getElementById("container");
//    main.onmouseenter = function () {
//        main.style.backgroundColor = "#888";
//    };

//    main.onmouseleave = function () {
//        main.style.backgroundColor = "";
//    };

//})();


(function () {


    //var uname = $("#username");
    //uname.text("Grigor Barbayan");

    //var main = $("#container");
    //main.on("mouseenter", function () {
    //    main.style = "background-color: #888;";
    //});
    //main.on("mouseleave", function () {
    //    main.style = "";
    //});

    //Testing the click function
    //var $menuItems = $("ul.menu li a");
    //$menuItems.on("click", function () {
    //    var cur = $(this);
    //    alert(cur.text());
    //});


    //Adding meaningfull stuff

    var $sidewrap = $("#sidebar , #wrapper");

    var $icon = $("#sidetog i.fa");

    $("#sidetog").on("click", function () {
        $sidewrap.toggleClass("hide-sidebar");
        if ($sidewrap.hasClass("hide-sidebar")) {
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        }
        else {
            $icon.addClass("fa-angle-left");
            $icon.removeClass("fa-angle-right");
        }
    });

})();

let inpuBtn = document.getElementById("sessioinInput");
let btnSession = document.getElementById("addSessionBtn");
let inpuMsg = document.getElementById("sessioinInputMsg"); 
let patt = /^\d{4}\/\d{4}$/;

inpuBtn.addEventListener("keyup", function () {
    if (!inpuBtn.value.match(patt)) {
        inpuMsg.innerHTML = "Doesn't match";
        btnSession.disabled = true;
    } else {
        inpuMsg.innerHTML = "";
        btnSession.disabled = false;
    }
})
inpuBtn.addEventListener("blur", function () {
        inpuBtn.value = "";
        inpuMsg.innerHTML = "";
        btnSession.disabled = false;
})