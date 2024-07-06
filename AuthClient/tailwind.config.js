/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  darkMode:'class',
  theme: {

    extend: {
      fontFamily: {
        mulish: 'Mulish, sans-serif',
        dms:'DM Sans, sans-serif',
      },
      fontSize:{
        'xl-important':['2.25rem',{important:true}]
      },
      minHeight: {
        'screen-minus-navbar': `calc(theme('height.screen') - theme('height.16'))`
      },
      colors: {
        primary: {
          50: '#ebf3fe',
          100: '#d4e7f9',
          200: '#a8d2f2',
          300: '#7bbde9',
          400: '#4ea8e0',
          500: '#25cba1',
          600: '#1da687',
          700: '#16996d',
          800: '#0f7c53',
          900: '#086f48',
          950: '#007d88'
        },
        light:{
          0:'#fcfcff',
          100:'#fcfcff',
          200:'#fcfcff',
          300:'#c2e0f9',
          400:'#abd1f3',
          500:'#aee2ff',
          600:'#095c8d',
          700:'#2f5288'
        },
        trueGreen: {
          400: '#67D822',
          600: '#48BC58',
        },
        yellow:{
          bg:'#fdfcf3'
        },
        lightPrimary:{
          0:'#edf6ef',
          50:'#e6f8ec',
          100:'#def5e5',
          200:'#bcead5',
          300:'#9ed5c5',
          400:'#8ec3b0'
        },
        surface: {
          0: '#ffffff',
          50: '#f8fafc',
          80:'#f8f9fa',
          100: '#ffffff',
          200: '#e2e8f0',
          300: '#cbd5e1',
          400: '#070b15',
          500: '#070b15',
          600: '#111322',
          700: '#111322',
          800: '#111322',
          900: '#111322',
          950: '#111322',
          1000:'#141728',
          1111:'#101729',
          2000:'#0d0e1a',
        },
        mintSplash:{
          deepblack:'#323534',
          black:'#454746',
          0:'#585a59',
          100:'#13c1a3',
          200:'#0de4a8',
          300:'#7af298',
          400:'#baf99c',
          500:'#f8f7ee'
        },
        forest:{
          0:'#050306',
          100:'#0f172a',
          200:'#412754',
          300:'#4cc35b',
          400:'#68da23'
        },
        west:{
          3:'#3a416f',
          2:'#f8f9fa',
          1:'#ffffff',
        },
        gang:{
          0:'#ffffff',
          100:'#f7f9fd',
          200:'#a08ffb',
          300:'#503bfa',
          400:'#c1f629',
          500:'#0a133b',
          600:'#0a133b',
        }
      }
    }
  },
  plugins: []
};
