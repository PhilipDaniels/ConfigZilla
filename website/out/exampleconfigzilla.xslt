<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
<xsl:output method="xml" indent="yes" omit-xml-declaration="yes" encoding="UTF-8"/>

  <!-- Default 'Identity' template copies all input to output -->
  <xsl:template match="node()|@*">
    <xsl:copy>
      <xsl:apply-templates select="node()|@*"/>
    </xsl:copy>
  </xsl:template>

  <!-- This template stamps the transformed config file with a comment.
       The default says who/when/where the file was generated. -->
  <xsl:template match="/">
    <xsl:comment> Built using configuration Release on machine NOFANWINBOX by Phil at 23-Feb-2014 17:09 </xsl:comment>
    <xsl:comment> Last Git commit was 73b8a0b74a83b27dcf9c4298e02110946eb3d835 on branch master </xsl:comment>
    <xsl:apply-templates />
  </xsl:template>

  <!-- Transforms from C:\repos\github\ConfigZilla\Samples\ConfigZilla\Transforms\AppSettings\AppSettings.czDefaults.xslt -->
  <xsl:template match="/configuration/appSettings/add[@key='Setting1']|/appSettings/add[@key='Setting1']">
    <add key="Setting1" value="Value1Release" />
  </xsl:template>

  <xsl:template match="/configuration/appSettings/add[@key='Setting2']|/appSettings/add[@key='Setting2']">
    <add key="Setting2" value="Value2Release" />
  </xsl:template>

  <!-- Replace <AppSettingsBlock /> with the whole set -->
  <xsl:template match="AppSettingsBlock" xml:space="preserve">
    <appSettings>
      <add key="Setting1" value="Value1Release" />
      <add key="Setting2" value="Value2Release" />
    </appSettings>
  </xsl:template>

  <!-- Transforms from C:\repos\github\ConfigZilla\Samples\ConfigZilla\Transforms\ConnectionStrings\ConnectionStrings.czDefaults.xslt -->
  <xsl:template match="/configuration/connectionStrings/add[@name='ConnStr1']|/connectionStrings/add[@name='ConnStr1']">
    <add name="ConnStr1" providerName="System.Data.SqlClient" connectionString="Data Source=PRDSQL;Initial Catalog=Db1;Integrated Security=True;" />
  </xsl:template>

  <xsl:template match="/configuration/connectionStrings/add[@name='ConnStr2']|/connectionStrings/add[@name='ConnStr2']">
    <add name="ConnStr2" providerName="System.Data.SqlClient" connectionString="Data Source=PRDSQL;Initial Catalog=Db2;Integrated Security=True;" />
  </xsl:template>

  <!-- Replace <ConnectionStringsBlock /> with the whole set -->
  <xsl:template match="ConnectionStringsBlock" xml:space="preserve">
    <connectionStrings>
      <add name="ConnStr1" providerName="System.Data.SqlClient" connectionString="Data Source=PRDSQL;Initial Catalog=Db1;Integrated Security=True;" />
      <add name="ConnStr2" providerName="System.Data.SqlClient" connectionString="Data Source=PRDSQL;Initial Catalog=Db2;Integrated Security=True;" />
    </connectionStrings>
  </xsl:template>

  <!-- Transforms from C:\repos\github\ConfigZilla\Samples\ConfigZilla\Transforms\log4net\log4net.czDefaults.xslt -->
  <!-- Set the conversion patterns in all appenders. Some of these settings are "expensive" according to the log4net documentation. -->
  <xsl:template match="//conversionPattern/@value">
    <xsl:attribute name="value">
      <xsl:value-of select="'%date [%thread] %-5level %20.20method - %message - %logger%newline'"/>
    </xsl:attribute>
  </xsl:template>

  <!-- Transforms from C:\repos\github\ConfigZilla\Samples\ConfigZilla\Transforms\log4net\log4net.Release.xslt -->
  <!-- Put the log file into a particular folder for Release mode. This could be done with a .targets file,
       but is done this way to demonstrate how *.Release.xslt inclusion works. -->
  <xsl:template match="/log4net/appender/file">
    <xsl:copy>
      <xsl:apply-templates select="@*"/>
      <xsl:attribute name="value">
        <xsl:value-of select="concat('C:\temp\', @value)"/>
      </xsl:attribute>
    </xsl:copy>
  </xsl:template>

  <!-- Transforms from C:\repos\github\ConfigZilla\Samples\ConfigZilla\Transforms\PaymentSettings\PaymentSettings.czDefaults.xslt -->
  <xsl:template match="/configuration/PaymentSettings/PaymentSystem/text()|/PaymentSettings/PaymentSystem/text()">
    <xsl:text>Wordlpay</xsl:text>
  </xsl:template>

  <xsl:template match="/configuration/PaymentSettings/URL/text()|/PaymentSettings/URL/text()">
    <xsl:text>https://worldpay.releasemode.example.com</xsl:text>
  </xsl:template>

  <xsl:template match="/configuration/PaymentSettings/Timeout/text()|/PaymentSettings/Timeout/text()">
    <xsl:text>30</xsl:text>
  </xsl:template>
  
  <!-- Replace <AppSettingsBlock /> with the whole set -->
  <xsl:template match="PaymentSettingsBlock" xml:space="preserve">
    <PaymentSettings>
      <PaymentSystem>Wordlpay</PaymentSystem>
      <URL>https://worldpay.releasemode.example.com</URL>
      <Timeout>30</Timeout>
    </PaymentSettings>
  </xsl:template>

  <!-- Transforms from C:\repos\github\ConfigZilla\Samples\ConfigZilla\Transforms\ReportingSettings\ReportingSettings.czDefaults.xslt -->
  <xsl:template match="/configuration/reportingSettings/@PageSize|/reportingSettings/@PageSize">
    <xsl:attribute name="PageSize">
      <xsl:value-of select="'20'"/>
    </xsl:attribute>
  </xsl:template>

  <xsl:template match="/configuration/reportingSettings/@Server|/reportingSettings/@Server">
    <xsl:attribute name="Server">
      <xsl:value-of select="'http://Release.example.com'"/>
    </xsl:attribute>
  </xsl:template>

  <xsl:template match="/configuration/reportingSettings/@RecipientEmail|/reportingSettings/@RecipientEmail">
    <xsl:attribute name="RecipientEmail">
      <xsl:value-of select="'phil@Release.example.com'"/>
    </xsl:attribute>
  </xsl:template>

  <!-- Transforms from C:\repos\github\ConfigZilla\Samples\ConfigZilla\Transforms\WebSettings\WebSettings.czDefaults.xslt -->
  <!-- Set debug flag -->
  <xsl:template match="/configuration/system.web/compilation/@debug|/system.web/compilation/@debug">
    <xsl:attribute name="debug">
      <xsl:value-of select="'false'"/>
    </xsl:attribute>
  </xsl:template>

</xsl:stylesheet>
