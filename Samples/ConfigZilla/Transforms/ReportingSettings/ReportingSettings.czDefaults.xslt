<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">

  <xsl:template match="/configuration/reportingSettings/@PageSize|/reportingSettings/@PageSize">
    <xsl:attribute name="PageSize">
      <xsl:value-of select="'$(rsPageSize)'"/>
    </xsl:attribute>
  </xsl:template>

  <xsl:template match="/configuration/reportingSettings/@Server|/reportingSettings/@Server">
    <xsl:attribute name="Server">
      <xsl:value-of select="'$(rsServer)'"/>
    </xsl:attribute>
  </xsl:template>

  <xsl:template match="/configuration/reportingSettings/@RecipientEmail|/reportingSettings/@RecipientEmail">
    <xsl:attribute name="RecipientEmail">
      <xsl:value-of select="'$(rsRecipientEmail)'"/>
    </xsl:attribute>
  </xsl:template>

</xsl:stylesheet>