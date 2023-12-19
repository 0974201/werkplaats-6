# Local Database

## Getting Started
As of now the deployment of this database is the responsibility of the user.
In future issues database will be migrated to the cloud. This migration will illuminate this responsibility.
- Installation of MongoDB Community Edition 
    - Go to the main [website of MongoDB](https://www.mongodb.com/docs/manual/installation/) and follow the installation steps.
- Specify the location of your database on you computer which mongod expects on your specific operating system
- The final stap is to activate a Mongod instance at the background, this package will act as an client to that server.

## Known Issues
- The Mongod Instance Sometimes crashes, however the  process will still run on the port, to reboot your mongod instance you need to kill that process.
  - Mongod Exit code: 48

## Tips 
- Use the [mongodb-compass](https://www.mongodb.com/try/download/atlascli) application to manage your databases on a GUI.