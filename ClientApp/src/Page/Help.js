import React, { useState, useEffect } from 'react'
import { Link, useLocation} from 'react-router-dom'


export default function Help() {
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
       {!hideLink && <Link to="/help" onClick={() => setHideLink(true)}>Help</Link>} 
       
       
    </div>
    
  )
}
