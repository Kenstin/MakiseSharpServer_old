#!/bin/bash
cd MakiseSharpServer/bin/Release/netcoreapp1.1
cd publish/runtimes
find . ! -name 'unix' -type d -exec rm -f -d {} + #remove everything except 'unix' directory
cd ../..
rm publish/*.pdb #remove debug unnecessary files

cd publish/ && tar czf ../package.tgz . && cd .. #pack everything in the 'publish' dir

export SSHPASS=$DEPLOY_PASS
echo "Sending the package..."
sshpass -e scp -o stricthostkeychecking=no package.tgz $DEPLOY_USER@$DEPLOY_HOST:$HOME
sleep 5
sshpass -e ssh $DEPLOY_USER@$DEPLOY_HOST pwd
sshpass -e ssh $DEPLOY_USER@$DEPLOY_HOST $HOME/deploy.sh