import React from "react";
import { NavLink } from "react-router-dom";

const Navbar = () => {
  return (
    <nav className="flex justify-between items-stretch text-xl ps-10 pe-10 pt-3 pb-3 bg-gray-300/40">
      <div className="items-start">
        <NavLink to="/">
          <p className="text-orange-500 font-bold">Bank</p>
        </NavLink>
      </div>

      <div className="items-end flex justify-between">
      <NavLink to="/register" className={({isActive}) => isActive ?
            'text-blue-500' : 'text-black'}>
          <p className=" font-semibold">Register</p>
        </NavLink>
        <NavLink to="/" className={({isActive}) => isActive ?
            'text-blue-500' : 'text-black'}>
          <p className=" font-semibold ps-4">Login</p>
        </NavLink>
      </div>
    </nav>
  );
};

export default Navbar;
