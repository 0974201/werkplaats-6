:: https://www.tutorialspoint.com/batch_script/batch_script_quick_guide.htm
@echo off
:: Start the Dashboard
CD ../src/front_end/dashboard
START npm install
START npm run dev

"C:\Users\zstig\AppData\Local\Programs\Python\Python310\python.exe"
"C:\Users\zstig\WebstormProjects\st-2324-1-d-wx1-t2-2324-wx1-bear\src\back_end\main.py"
:: Start Python
:: CD ../../back_end
:: DIR
:: START main.py
echo Crane Started
