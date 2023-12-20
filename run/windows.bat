:: https://www.tutorialspoint.com/batch_script/batch_script_quick_guide.htm
@echo off
:: Start the Dashboard
CD ../src/front_end/dashboard
START npm install
START npm run dev

echo Dashboard Started
