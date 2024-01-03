import React from "react";

const Register = () => {
  return (
    <div className="flex flex-col flex-grow items-center justify-center">
      <div className="m-6 max-w-md p-6 bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700">
        <p className="font-bold text-red-600 text-xl text-center mb-4">
          Register
        </p>
        <form className="m-3">
          <div className="flex justify-between gap-3">
            <div className="mb-4">
              <label
                className="block text-gray-700 text-md font-bold mb-2"
                htmlFor="name"
              >
                Name
              </label>
              <input
                className="shadow appearance-none border rounded-lg w-full py-2 px-3 text-xs text-slate-500 leading-tight focus:outline-none focus:border-blue-500 hover:border-blue-500"
                id="username"
                type="text"
                placeholder="Name"
                required
              />
            </div>

            <div className="mb-4">
              <label
                className="block text-gray-700 text-md font-bold mb-2"
                htmlFor="Citizenship"
              >
                Citizenship No
              </label>
              <input
                className="shadow appearance-none border rounded-lg w-full py-2 px-3 text-xs text-slate-500 leading-tight focus:outline-none focus:border-blue-500 hover:border-blue-500"
                id="username"
                type="text"
                placeholder="Citizenship No"
                required
              />
            </div>
          </div>
          <div className="mb-4">
            <label
              className="block text-gray-700 text-md font-bold mb-2"
              htmlFor="Email"
            >
              Email
            </label>
            <input
              className="shadow appearance-none border rounded-lg w-full py-2 px-3 text-xs text-slate-500 leading-tight focus:outline-none focus:border-blue-500 hover:border-blue-500"
              id="username"
              type="text"
              placeholder="Email"
              required
            />
          </div>
          <div className="mb-4">
            <label
              className="block text-gray-700 text-md font-bold mb-2"
              htmlFor="Dob"
            >
              DOB
            </label>
            <input
              className="shadow appearance-none border rounded-lg w-full py-2 px-3 text-sm text-slate-500 leading-tight focus:outline-none focus:border-blue-500 hover:border-blue-500"
              id="username"
              type="date"
              required
            />
          </div>
          <div className="mb-4">
            <label
              className="block text-gray-700 text-md font-bold mb-2"
              htmlFor="password"
            >
              Password
            </label>
            <input
              className="shadow appearance-none border rounded-lg w-full py-2 px-3 mb-2 text-sm leading-tight focus:outline-none focus:border-blue-500 hover:border-blue-500"
              id="password"
              type="password"
              placeholder="Password"
              required
            />
          </div>
          <div className="mb-4">
            <label
              className="block text-gray-700 text-md font-bold mb-2"
              htmlFor="confirmPassword"
            >
              Confirm Password
            </label>
            <input
              className="shadow appearance-none border rounded-lg w-full py-2 px-3 mb-2 text-sm leading-tight focus:outline-none focus:border-blue-500 hover:border-blue-500"
              id="password"
              type="password"
              placeholder="Confirm Password"
              required
            />
          </div>
          <div className="flex items-center justify-center">
            <button
              className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
              type="submit"
            >
              Register
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Register;
