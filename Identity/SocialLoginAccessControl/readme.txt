SocialLoginAccessControl Sample

This sample illustrates the following:
- accepting username password login
- storing username and profile with asp.net membership database
- also allowing association of account profile with social login such as twitter, facebook, google, windows live
- using access control to login to facebook, google, windows live
- using social sts to login to twitter (through access control)

To run this sample with your own access control account you'll need to:
- set up access control namespace in your own account
- replace all instances of bustacloud.accesscontrol with your own namespace.accesscontrol
- replace the certificate thumbprint in the identity section of the web.config with your and set that up with access control
- set up access control to support this the identity providers you want to test with
