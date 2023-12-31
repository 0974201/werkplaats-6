# create a Python Virtual Environment in Project folder
python3 -m venv env

# activate Python Virtual Environment
. env/bin/activate

# install Pythondependencies
python3 setup.py install 

# change to src directory
cd src

# execute package as script
python3 -m back_end

# deactivate Python Virtual Environment
deactivate

cd ..

# Concept for including Frontend
# python3 -m venv env

# . env/bin/activate

# python3 setup.py install 

# cd src/front_end/dashboard; npm install; 

# npm run dev & cd ../../; python3 -m back_end

# deactivate