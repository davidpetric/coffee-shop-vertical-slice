/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
    "./projects/**/*.{html,ts}",
    "./projects/auth-lib/**/*.{html,ts}",
  ],

  theme: {
    extend: {},
    fontFamily: {
      sans: ["Poppins"],
    },
  },
  plugins: [],
};
