@echo off
REM https://stackoverflow.com/questions/21488845/how-can-i-generate-a-self-signed-certificate-with-subjectaltname-using-openssl
openssl x509 -in cert.pem -text -noout
@echo on
