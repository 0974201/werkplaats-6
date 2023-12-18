:: https://www.tutorialspoint.com/batch_script/batch_script_quick_guide.htm
@echo off
:: Start the Dashboard
echo cd
echo Crane Started
CD ../src/front_end/dashboard
START npm run dev
CD ../../back_end/broker
DIR
START client.py

