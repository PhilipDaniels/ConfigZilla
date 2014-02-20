<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="xml" indent="yes" omit-xml-declaration="yes"/>

  <xsl:template match="/configuration/PaymentSettings/PaymentSystem/text()|/PaymentSettings/PaymentSystem/text()">
    <xsl:text>$(payPaymentSystem)</xsl:text>
  </xsl:template>

  <xsl:template match="/configuration/PaymentSettings/URL/text()|/PaymentSettings/URL/text()">
    <xsl:text>$(payURL)</xsl:text>
  </xsl:template>

  <xsl:template match="/configuration/PaymentSettings/Timeout/text()|/PaymentSettings/Timeout/text()">
    <xsl:text>$(payTimeout)</xsl:text>
  </xsl:template>
  
  <!-- Replace <AppSettingsBlock /> with the whole set -->
  <xsl:template match="PaymentSettingsBlock" xml:space="preserve">
    <PaymentSettings>
      <PaymentSystem>$(payPaymentSystem)</PaymentSystem>
      <URL>$(payURL)</URL>
      <Timeout>$(payTimeout)</Timeout>
    </PaymentSettings>
  </xsl:template>

</xsl:stylesheet>