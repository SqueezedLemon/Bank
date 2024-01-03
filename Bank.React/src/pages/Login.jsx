import React from "react";

const Login = () => {
  return (
    <div className="flex flex-col flex-grow items-center justify-center">
      <div className="m-10 max-w-xl p-8 bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700">
        <p className="font-bold text-red-600 text-2xl text-center mb-6">
          Login
        </p>
        <form className="m-5">
          <div className="mb-6">
            <label
              className="block text-gray-700 text-lg font-bold mb-2"
              htmlFor="email"
            >
              Email
            </label>
            <input
              className="shadow appearance-none border rounded-lg w-full py-3 px-4 text-lg text-slate-500 leading-tight focus:outline-none focus:border-blue-500 hover:border-blue-500"
              id="username"
              type="text"
              placeholder="Email"
              required
            />
          </div>
          <div className="mb-6">
            <label
              className="block text-gray-700 text-lg font-bold mb-2"
              htmlFor="password"
            >
              Password
            </label>
            <input
              className="shadow appearance-none border rounded-lg w-full py-3 px-4 mb-3 text-lg leading-tight focus:outline-none  focus:border-blue-500 hover:border-blue-500"
              id="password"
              type="password"
              placeholder="Password"
              required
            />
            {/* <p className="text-red-500 text-xs italic">
          Please choose a password.
        </p> */}
          </div>
          <div className="flex items-center justify-center">
            <button
              className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-5 rounded focus:outline-none focus:shadow-outline"
              type="submit"
            >
              Sign In
            </button>
          </div>
          <div className="flex items-center justify-center mt-4">
            <a
              className="inline-block align-baseline font-bold text-base text-blue-500 hover:text-blue-800 hover:underline"
              href="#"
            >
              Forgot Password?
            </a>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Login;
