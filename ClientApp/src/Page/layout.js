import React from "react";
import Navigation from "../Components/Home/Navigation";
import Footer from "../Components/Home/Footer";

const Layout = ({ children }) => {
    return (
        <div>
          <div id="navigation">
            <Navigation />
          </div>
      
          <div id="content">
            {children}
          </div>
      
          <div id="footer">
            <Footer />
          </div>
        </div>
      );
      
};

export default Layout;