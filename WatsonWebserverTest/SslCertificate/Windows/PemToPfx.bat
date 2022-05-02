@echo off
REM https://stackoverflow.com/questions/808669/convert-a-cert-pem-certificate-to-a-pfx-certificate
openssl pkcs12 -inkey priv.key -in cert.pem -export -out cert.pfx
@echo on
