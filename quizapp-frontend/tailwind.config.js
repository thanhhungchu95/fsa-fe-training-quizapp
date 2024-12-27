/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      animation: {
        'spin-slower': 'spin 1.5s linear infinite',
        'spin-slowest': 'spin 3s linear infinite',
      },
      borderWidth: {
        '12': '12px',
        '16': '16px',
        '24': '24px',
      }
    },
  },
  plugins: [],
}

