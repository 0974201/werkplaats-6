import os
from setuptools import setup

def get_readme(README):
    return open(os.path.join(os.path.dirname(__file__), README)).read()

setup(
    name = "st-2324-1-d-wx1-t2-2324-wx1-bear",
    version = "1.0",
    author = "Miro Noordzij, Zasha, Tristan, Andy, Quylian, Suman, Yassine, Martijn",
    author_email = "1041400@hr.nl",
    description = ("An abstracted digital twin of an STS-crane."),
    license = "MIT",
    keywords = "STS-Crane Digital Twin",
    url = "https://github.com/howest-gp-wpl/st-2324-1-d-wx1-t2-2324-wx1-bear",
    packages=['src'],
    long_description=get_readme('README'),
    classifiers=[
        "Development Status :: 3 - Alpha",
        "Intended Audience :: Developers",
        "Operating System :: OS Independent",
        "License :: MIT License",
        "Programming Language :: Python 3 :: JavaScript :: C#",
    ],
    install_requires=[
        'pymongo[srv]==4.6.1',
        'python-dotenv==1.0.0',
        'paho-mqtt==1.6.1',
        'pytz==2023.3.post1',
   ],
)