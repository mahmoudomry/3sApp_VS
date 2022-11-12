inlineSVG.init(
  {
    svgSelector: "img.svg-inline", // the class attached to all images that should be inlined
    initClass: "js-inlinesvg", // class added to <html>
  },
  function () {
    console.log("All SVGs inlined");
  }
);
AOS.init();
