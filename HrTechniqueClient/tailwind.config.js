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
          100: '#d4f0f9',
          200: '#a8e1f2',
          300: '#7bc8e9',
          400: '#4ebee0',
          500: '#258bcb',
          600: '#1da68d',
          700: '#16997a',
          800: '#0f527c',
          900: '#083b6f',
          950: '#004488'
        },
        light:{
          0:'#fcfcff',
          100:'#fcfcff',
          200:'#fcfcff',
          300:'#c2f9e4',
          400:'#abf3cf',
          500:'#aeffe7',
          600:'#10a997',
          700:'#2f8881'
        },
        trueGreen: {
          400: '#22d8a1',
          600: '#48bcae',
        },
        lightPrimary:{
          0:'#edf6ef',
          50:'#24e5a1',
          100:'#40a18f',
          200:'#bcead5',
          300:'#9ed5c5',
          400:'#8ec3b0'
        },
        yellow:{
          bg:'#fdfcf3'
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
        west:{
          3:'#3a416f',
          2:'#f8f9fa',
          1:'#ffffff',
        },
        mintSplash:{
          deepblack:'#323534',
          black:'#454746',
          0:'#585a59',
          100:'#13c1ad',
          200:'#0de4c4',
          300:'#7af2dc',
          400:'#9cf9e0',
        },
        forest:{
          0:'#050306',
          100:'#0f172a',
          200:'#412754',
          300:'#4cc39f',
          400:'#23daa9'
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
