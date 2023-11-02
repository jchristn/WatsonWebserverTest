@echo off
REM https://www.ssl.com/how-to/manually-generate-a-certificate-signing-request-csr-using-openssl/
REM https://stackoverflow.com/questions/21488845/how-can-i-generate-a-self-signed-certificate-with-subjectaltname-using-openssl
openssl req -new -x509 -key priv.key -sha256 -out cert.pem -days 730
@echo on

