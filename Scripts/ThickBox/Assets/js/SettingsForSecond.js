window.addEvent("domready", function() {
    var gallery1 = new slideGallery($$("div.gallery")[1], {
        steps: 4,
        speed: 1000,
        mode: "line",
        onStart: function() {
            $$("span.info")[0].innerHTML = parseInt(this.current + 1) + " from " + this.items.length;
        },
        onPlay: function() {
            $$("span.info")[0].innerHTML = parseInt(this.current + 1) + " from " + this.items.length;
        }
    });
});