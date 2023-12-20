import React, { useState, useEffect } from 'react'
import { Link, useLocation} from 'react-router-dom'


export default function AboutUs() {
    const location = useLocation();
    const [hideLink, setHideLink] = useState(false);
    
    useEffect(() => {
        if (location.pathname === '/aboutus') {
          setHideLink(true);
        } else {
          setHideLink(false);
        }
      }, [location]);
  

  return (
    <div>
       {!hideLink && <Link to="/aboutus"onClick={() => setHideLink(true)}>About Us</Link>} 
       
       
    </div>
    
  )
}
