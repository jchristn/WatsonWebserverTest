@echo off
REM https://stackoverflow.com/questions/21488845/how-can-i-generate-a-self-signed-certificate-with-subjectaltname-using-openssl
openssl req -new -x509 -v3 -key priv.key -out cert.pem -days 730 -config ./openssl.cnf
@echo on
