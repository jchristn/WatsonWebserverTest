# Generating a Self-Signed SSLv3 Certificate using OpenSSL

## Step 1 - Modify openssl.cnf

I do not claim to be an expert in OpenSSL, and I used public sources for creation of a v3 self-signed certificate.  These steps require modification of ```openssl.cnf```.  Please evaluate these changes carefully for your environment.

Refer to [this stackoverflow article](https://stackoverflow.com/questions/21488845/how-can-i-generate-a-self-signed-certificate-with-subjectaltname-using-openssl) for specifics.

You can, of course, duplicate your ```openssl.cnf``` file and modify the copy.  You would then need to point your ```openssl``` command to the new config file using ```-config [filename]```.

## Step 2 - Generate a Private Key
```
openssl genrsa -out priv.key 4096
```

## Step 3 - Generate CSR
```
openssl req -new -x509 -key priv.key -sha256 -out cert.pem -days 730
```

## Step 4 - Generate Certificate
```
openssl req -new -x509 -v3 -key priv.key -out cert.pem -days 730
```

## Step 5 - Inspect Certificate
```
openssl x509 -in cert.pem -text -noout
```
Note the following:
```
Certificate:
    Data:
        Version: 3 (0x2) <--------
        Serial Number:
            48:dd:0b:c1:e4:94:72:86:ac:88:f1:d0:73:39:24:80:33:ff:a5:13
```

## Step 6 (Windows only) - Convert PEM to PFX
```
openssl pkcs12 -inkey priv.key -in cert.pem -export -out cert.pfx
```

## Certificate Installation

### Windows

1) Open ```mmc.exe``` (Start > Run > mmc.exe)
2) File -> Add/Remove Snap-in
3) Add the ```Certificates``` Snap-in
4) Expand ```Certificates``` -> ```Personal``` -> ```Certificates```
5) Right-click ```Certificates``` -> ```All Tasks``` -> ```Import```
6) Select your ```.pfx``` file
7) Complete the import wizard (generally using default settings and placing in the ```Personal``` store)
8) Double-click the newly installed certificate
9) In the ```Details``` tab, copy the value shown for ```Thumbprint``` (you will need this)
10) Using an administrator command prompt, install the certificate for your listener prefix using ```netsh```
```
netsh http add sslcert ipport="0.0.0.0:443" certhash="[thumbprint]" appid="{00000000-0000-0000-0000-000000000000}" certstore=My
```
NOTE: the thumbprint, when copied from MMC, may contain extranneous characters that are not visible.  This may cause an error.

11) Verify SSL certificate installation using ```netsh http show sslcert```
12) Add a binding using ```netsh http add urlacl url=https://host.domain.com:443/ user=everyone listen=yes```

You can now instantiate Watson Webserver using SSL on the specified listener.

To validate using cURL, you may have to pass ```-k``` or ```--insecure```:
```
curl https://127.0.0.1:8000 -k
```

### Linux

This portion was tested on Ubuntu v20.04.

