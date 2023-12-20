import React, { useRef, useEffect } from "react";
import ReactDOM from "react-dom";
import AboutUs from "../../Page/Aboutus";
import Help from "../../Page/Help";


const Footer = () => {
  const footerRef = useRef(null);
  

  useEffect(() => {
    if (footerRef.current) {
      ReactDOM.createPortal(<Footer />, footerRef.current);
    }
  }, []);
  

  return (
    <div ref={footerRef}>
        <AboutUs />
        <Help/>
    </div>
  );
};

export default Footer;
